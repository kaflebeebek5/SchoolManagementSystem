ALTER PROCEDURE Auth.spAuthSetup 
@UserId INT=NULL,
@Flag NVARCHAR(50)=NULL,
@RoleId INT=NULL,
@ParentId INT=NULL,
@MenuId INT=NULL,
@CreatedBy INT=NULL,
@JsonData NVARCHAR(MAX)=NULL
AS 
IF @Flag='GetUserRole'
 BEGIN
   IF @UserId=0
    BEGIN
	 SELECT * From Auth.tblRole WITH(NOLOCK) where Status=1
	END
   ELSE
	BEGIN
      SELECT RoleId INTO #ROLEASSIGN FROM Auth.tblUserRole MR WITH(NOLOCK) 
		WHERE UserID=@UserId
		;WITH CTE AS (
		SELECT cast(1 as bit) Ischecked,UR.RoleId,RoleName from Auth.tblUserRole UR WITH(NOLOCK)
		LEFT JOIN Auth.tblRole R WITH(NOLOCK) ON UR.RoleId = R.RoleId
		WHERE UserID=@UserId and Status=1
		UNION ALL
		SELECT cast(0 as bit) Ischecked,R.RoleId,R.RoleName from Auth.tblRole R WITH(NOLOCK)
		WHERE R.RoleId not in (select ma.RoleId from #ROLEASSIGN ma ) and Status=1
		) 
		SELECT * FROM CTE ORDER BY RoleId
	 END
	


  IF @Flag='GetMenuRole' ------Get Menu by Role ID----
   BEGIN
     SELECT MenuId INTO #MENUASSIGN FROM Auth.tblMenuRole MR WITH(NOLOCK) 
		WHERE RoleId = @RoleId

		;WITH CTE AS (
		SELECT cast(1 as bit) Ischecked,ML.MenuId,ML.MenuName,ML.ParentID,M.MenuName as ParentMenu,ML.Status  from Auth.tblMenuRole MR WITH(NOLOCK)
		LEFT JOIN Auth.tblMenu ML WITH(NOLOCK) ON MR.MenuId = ML.MenuId
		LEFT JOIN Auth.tblMenu M WITH(NOLOCK) ON M.MenuID=ML.ParentID
		WHERE RoleId=@RoleId
		UNION ALL
		SELECT cast(0 as bit) Ischecked,MR.MenuId,MR.MenuName,MR.ParentID,M.MenuName as ParentMenu,MR.Status  from Auth.tblMenu MR WITH(NOLOCK)
		--LEFT JOIN Auth.tblMenuRole ML WITH(NOLOCK) ON MR.MenuId = ML.MenuId
		LEFT JOIN Auth.tblMenu M WITH(NOLOCK) ON M.MenuID=MR.ParentID
		WHERE MR.MenuId not in (select ma.MenuId from #MENUASSIGN ma ) --and RoleId =@RoleId
		) 
		SELECT DISTINCT * FROM CTE
		WHERE Status=1
		ORDER BY MenuId 
  END


 IF @Flag='GetPermissionRole' ------Get Permission by Role ID----
   BEGIN
     SELECT PermissionId INTO #PERMISSIONASSIGN FROM Auth.tblPermissionRole MR WITH(NOLOCK) 
		WHERE RoleId = @RoleId
	;WITH CTE AS (
		SELECT cast(1 as bit) Ischecked,PR.PermissionId,PermissionValue,P.MenuId,MenuName from Auth.tblPermissionRole PR WITH(NOLOCK)
		LEFT JOIN Auth.tblPermission P WITH(NOLOCK) ON PR.PermissionId = P.PermissionId
		LEFT JOIN Auth.tblMenu M WITH(NOLOCK) ON M.MenuID=P.MenuId
		WHERE PR.RoleId=@RoleId --and P.MenuId in(select MenuId from Auth.tblMenuRole where RoleId=@RoleId)
		UNION ALL
		SELECT cast(0 as bit) Ischecked,PR.PermissionId,PR.PermissionValue,PR.MenuId,MenuName from Auth.tblPermission PR WITH(NOLOCK)
		--LEFT JOIN Auth.tblPermissionRole P WITH(NOLOCK) ON PR.PermissionId = P.PermissionId
		LEFT JOIN Auth.tblMenu M WITH(NOLOCK) ON M.MenuID=PR.MenuId
		WHERE PR.PermissionId not in (select ma.PermissionId from #PERMISSIONASSIGN ma ) --and PR.MenuId in(select MenuId from Auth.tblMenuRole where RoleId=@RoleId) --and RoleId =@RoleId
		) 
		SELECT * FROM CTE ORDER BY PermissionId
  END


  IF @Flag='UpdateUserRole'
   BEGIN
     BEGIN TRY
	  BEGIN TRAN
	      Delete From Auth.tblUserRole where UserID=@UserId
		  INSERT INTO Auth.tblUserRole(UserID,RoleId,CreatedBy)
	      SELECT @UserId,RoleId,CreatedBy
	      FROM OPENJSON(@JsonData)
	      WITH(
		     RoleId INT '$.RoleId',
		     CreatedBy NVARCHAR(MAX) '$.CreatedBy'
		     );
	   COMMIT TRAN
	   END TRY
	   BEGIN CATCH
			ROLLBACK TRAN 
			;THROW 
      END CATCH
   END

 --------Update Menu Role Wise-----
   IF @Flag='UpdateMenuRole'
    BEGIN
	 BEGIN TRY
	  BEGIN TRAN
	   IF @ParentId=-1
	    BEGIN
		     DELETE FROM Auth.tblMenuRole where RoleId=@RoleId
		     INSERT INTO Auth.tblMenuRole(MenuId,RoleId,CreatdBy,CreatedDate)
		     SELECT MenuId,@RoleId,1,GETDATE()
	         FROM OPENJSON(@JsonData)
	         WITH(
	           MenuId NVARCHAR(100) '$.MenuId',
		        RoleId INT '$.RoleId'
		      );
		  END
	    ELSE
	      BEGIN
		    create table #menuRole(MenuId int,RoleId int,ParentId int,CreatedBy int ,CreatedDate Datetime)
	      --Delete From Auth.tblMenuRole where RoleId=@RoleId
		 -- INSERT INTO Auth.tblMenuRole(MenuId,RoleId,CreatdBy,CreatedDate)
		   INSERT INTO #menuRole(MenuId,RoleId,ParentId,CreatedBy,CreatedDate)
	       SELECT MenuId,@RoleId,@ParentId,1,GETDATE()
	       FROM OPENJSON(@JsonData)
	       WITH(
	         MenuId NVARCHAR(100) '$.MenuId',
		     RoleId INT '$.RoleId',
			 ParentId int '$.ParentId'
		     );

			 --Declare @MenuRoleId int 

		
			 --select distinct @MenuRoleId = RoleId from #menuRole 
			
			 SELECT M.MenuId INTO #AssignRole FROM [Auth].tblMenu M WITH (NOLOCK)
			 WHERE
				ParentID=@ParentId
		
		      DELETE FROM [Auth].tblMenuRole WHERE MenuId  in (SELECT ar.MenuID from #assignRole ar) and RoleId=@RoleId --and RoleId=@MenuRoleId

		     INSERT INTO [Auth].tblMenuRole(RoleId,MenuId,CreatdBy,CreatedDate)
		     SELECT RoleId,MenuId,CreatedBy,CreatedDate FROM #menuRole
		   END
	   COMMIT TRAN
	   END TRY
	   BEGIN CATCH
			ROLLBACK TRAN 
			;THROW 
       END CATCH
   END

   IF @Flag='J' --------UPDATE PERMISSION ROLE WISE-----
    BEGIN
	 BEGIN TRY
	  BEGIN TRAN
	    IF @MenuId=-1
		 BEGIN
	       Delete From Auth.tblPermissionRole where RoleId=@RoleId
		   INSERT INTO Auth.tblPermissionRole(PermissionId,RoleId)
	       SELECT PermissionId,@RoleId
	       FROM OPENJSON(@JsonData)
	       WITH(
	          PermissionId NVARCHAR(100) '$.PermissionId'
		      );
		  END
		 ELSE
		  BEGIN
		   CREATE TABLE #permissionRole(PermissionId INT,RoleId INT,MenuId INT,CreatedBy INT ,CreatedDate DATETIME)
	         --Delete From Auth.tblMenuRole where RoleId=@RoleId
		    -- INSERT INTO Auth.tblMenuRole(MenuId,RoleId,CreatdBy,CreatedDate)
		   INSERT INTO #permissionRole(PermissionId,RoleId,MenuId,CreatedBy,CreatedDate)
	       SELECT PermissionId,@RoleId,@MenuId,@CreatedBy,GETDATE()
	       FROM OPENJSON(@JsonData)
	       WITH(
	          PermissionId NVARCHAR(100) '$.PermissionId'
		      );

			 SELECT M.PermissionId INTO #AssignPermission FROM [Auth].tblPermission m WITH (NOLOCK)
			 WHERE
				m.MenuId=@MenuId
		
		      DELETE FROM [Auth].tblPermissionRole WHERE PermissionId  in (SELECT ar.PermissionId FROM #AssignPermission ar) and RoleId=@RoleId

		     INSERT INTO [Auth].tblPermissionRole(RoleId,PermissionId,CreatedBy,CreatedDate)
		     SELECT RoleId,PermissionId,CreatedBy,CreatedDate from #permissionRole
		  END
	   COMMIT TRAN
	 END TRY
	 BEGIN CATCH
	 	   ROLLBACK TRAN
           ;THROW 
     END CATCH
    END
  
 END
