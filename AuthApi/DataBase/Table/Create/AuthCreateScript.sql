CREATE SCHEMA Auth

CREATE TABLE Auth.tblUser(
	UserId int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	UserName nvarchar(50) NOT NULL,
	Password nvarchar(500) NULL,
	Name nvarchar(50) NULL,
	Address nvarchar(50) NULL,
	ContactNo varchar(50) NULL,
	Email nvarchar(50) NULL,
	ValidFrom date NULL,
	ValidTo date NULL,
	Photo varchar(500) NULL,
	Status int NULL,
	CreatedBy int NULL,
	CreatedDate datetime NULL,
	ModifiedBy int NULL,
	ModifiedDate datetime NULL,
	IsLocked bit NULL,
	MobileNo nvarchar(20) NULL,
	JoinDate datetime NULL
)

CREATE TABLE Auth.tblRole(
	RoleId int PRIMARY KEY IDENTITY(1,1)  NOT NULL,
	RoleName nvarchar(256) NULL,
	CreatedBy int NULL,
	CreatedDate datetime NULL,
	ModifiedBy int NULL,
	ModifiedDate datetime NULL,
	Status int NULL,
)


CREATE TABLE Auth.tblMenu(
	MenuID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	MenuName nvarchar(100) NULL,
	MenuLabel varchar(50) NULL,
	FormName varchar(100) NULL,
	ShortCutKey varchar(20) NULL,
	ParentID int NULL,
	Status int NULL,
	URL nvarchar(100) NULL,
	MenuIcon nvarchar(100) NULL,
	CreatedBy int NULL,
	CreatedDate date NULL,
	ModifiedBy int NULL,
	ModifiedDate date NULL,
	MenuOrder int NULL,
)

CREATE TABLE Auth.tblPermission(
	PermissionId  int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	PermissionType nvarchar(100) NULL,
	PermissionValue nvarchar(100) NULL,
	PermissionDescription nvarchar(100) NULL,
	MenuId int NULL,
)


CREATE TABLE Auth.tblMenuRole(
	ID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	RoleId int FOREIGN KEY REFERENCES Auth.tblRole(RoleId) NULL,
	MenuId int FOREIGN KEY REFERENCES Auth.tblMenu(MenuId) NULL,
	CreatdBy int NULL,
	CreatedDate datetime NULL
)

CREATE TABLE Auth.tblPermissionRole(
	Id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	RoleId int FOREIGN KEY REFERENCES Auth.tblRole(RoleId) NULL,
	PermissionId int FOREIGN KEY REFERENCES Auth.tblPermission(PermissionId) NULL,
	CreatedBy int NULL,
	CreatedDate datetime NULL,
)



CREATE TABLE Auth.tblUserRole(
	ID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	UserID int FOREIGN KEY REFERENCES Auth.tblUser(UserId) NOT NULL ,
	RoleId int FOREIGN KEY REFERENCES Auth.tblRole(RoleId) NOT NULL,
	CreatedDate datetime NULL,
	CreatedBy int NULL,
)

