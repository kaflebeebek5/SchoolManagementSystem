INSERT INTO Auth.tblPermission(PermissionType,PermissionValue,PermissionDescription)
SELECT 'Permission','User.R','Read User'
UNION ALL
SELECT 'Permission','User.U','Update User'
UNION ALL
SELECT 'Permission','User.D','Delete User'
UNION ALL
SELECT 'Permission','PER.C','Create Permission'
UNION ALL
SELECT 'Permission','PER.R','Read Permission'
UNION ALL
SELECT 'Permission','PER.U','Update Permission'
UNION ALL
SELECT 'Permission','PER.D','Delete Permission'
UNION ALL
SELECT 'Permission','Menu.C','Create Menu'
UNION ALL
SELECT 'Permission','Menu.R','Read Menu'
UNION ALL
SELECT 'Permission','Menu.U','Update Menu'
UNION ALL
SELECT 'Permission','Menu.D','Delete Menu'
UNION ALL
SELECT 'Permission','Role.C','Create Role'
UNION ALL
SELECT 'Permission','Role.R','Read Role'
UNION ALL
SELECT 'Permission','Role.U','Update Role'
UNION ALL
SELECT 'Permission','Role.D','Delete Role'
UNION ALL
SELECT 'Permission','MR.R','Read Menu Role'
UNION ALL
SELECT 'Permission','MR.U','Update Menu Role'
UNION ALL
SELECT 'Permission','PR.R','Read Permission Role'
UNION ALL
SELECT 'Permission','PR.U','Update Permission Role'
UNION ALL
SELECT 'Permission','UR.R','Read User Role'
UNION ALL
SELECT 'Permission','UR.U','Update User Role'



INSERT INTO Auth.tblPermissionRole(PermissionId,RoleId,CreatedBy,CreatedDate)
SELECT PermissionId,1,6,GETDATE() FROM Auth.tblPermission



