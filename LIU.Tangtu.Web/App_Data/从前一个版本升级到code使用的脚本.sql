--修改用户
IF EXISTS(SELECT * FROM sys.indexes WHERE name='PK_dbo.sys_userInfo')
BEGIN    
	ALTER TABLE dbo.sys_userInfo DROP [PK_dbo.sys_userInfo]
END
go
ALTER TABLE dbo.sys_userInfo ALTER COLUMN gkey VARCHAR(36) NOT NULL 
GO

	ALTER TABLE dbo.sys_userInfo  ALTER COLUMN  gkey VARCHAR(36)  
	ALTER TABLE	[dbo].[eb_chapter]  ALTER COLUMN  gInsertUserKey VARCHAR(36)  

	
	ALTER TABLE	[dbo].[eb_novel]  ALTER COLUMN  gInsertUserKey VARCHAR(36)  
	ALTER TABLE	[dbo].[eb_novel]  ALTER COLUMN  gUpdateUserKey VARCHAR(36)  

	ALTER TABLE	[dbo].[sys_goldFlow]  ALTER COLUMN  gUserKey VARCHAR(36)  

	
	ALTER TABLE	[dbo].[sys_menu]  ALTER COLUMN  gInsertKey VARCHAR(36)  
	ALTER TABLE	[dbo].[sys_menu]  ALTER COLUMN  gUpdateKey VARCHAR(36)  

	ALTER TABLE	[dbo].[sys_messageBoard]  ALTER COLUMN  gInsertKey VARCHAR(36)  
	ALTER TABLE	[dbo].[sys_messageBoard]  ALTER COLUMN  gReplyKey VARCHAR(36)  

	ALTER TABLE	[dbo].[sys_Notice]  ALTER COLUMN  gCreateId VARCHAR(36)  


	ALTER TABLE	[dbo].[sys_notifyMessage]  ALTER COLUMN  gSendKey VARCHAR(36)  
	ALTER TABLE	[dbo].[sys_notifyMessage]  ALTER COLUMN  gReceiveKey VARCHAR(36)  

	ALTER TABLE	[dbo].[sys_role]  ALTER COLUMN  gInsertKey VARCHAR(36)  
	ALTER TABLE	[dbo].[sys_role]  ALTER COLUMN  gUpdateKey VARCHAR(36)  
	
	ALTER TABLE	[dbo].[sys_role_menu]  ALTER COLUMN  gInsertKey VARCHAR(36)  
	ALTER TABLE	[dbo].[sys_role_menu]  ALTER COLUMN  gUpdateKey VARCHAR(36)  

	ALTER TABLE	[dbo].[sys_user_goods]  ALTER COLUMN  gUserKey VARCHAR(36)  

	ALTER TABLE	[dbo].[sys_validate]  ALTER COLUMN  gUserKey VARCHAR(36)  
	ALTER TABLE	[dbo].[sys_vipHistory]  ALTER COLUMN  gUserKey VARCHAR(36)  

	go
DECLARE @iii BIGINT,@id VARCHAR(36)
SET @iii=1000001
DECLARE sss CURSOR
FOR SELECT gkey FROM dbo.sys_userInfo
OPEN sss
FETCH NEXT from sss INTO @id /* 读取第1行数据*/
　　WHILE @@FETCH_STATUS = 0 /* 用WHILE循环控制游标活动 */
　　BEGIN
　　	UPDATE dbo.sys_userInfo SET gkey=@iii WHERE gkey=@id
	UPDATE	[dbo].[eb_chapter] SET gInsertUserKey=@iii WHERE gInsertUserKey=@id
	
	UPDATE	[dbo].[eb_novel] SET gInsertUserKey=@iii WHERE gInsertUserKey=@id
	UPDATE	[dbo].[eb_novel] SET gUpdateUserKey=@iii WHERE gUpdateUserKey=@id

	UPDATE	[dbo].[sys_goldFlow] SET gUserKey=@iii WHERE gUserKey=@id

	
	UPDATE	[dbo].[sys_menu] SET gInsertKey=@iii WHERE gInsertKey=@id
	UPDATE	[dbo].[sys_menu] SET gUpdateKey=@iii WHERE gUpdateKey=@id

	UPDATE	[dbo].[sys_messageBoard] SET gInsertKey=@iii WHERE gInsertKey=@id
	UPDATE	[dbo].[sys_messageBoard] SET gReplyKey=@iii WHERE gReplyKey=@id

	UPDATE	[dbo].[sys_Notice] SET gCreateId=@iii WHERE gCreateId=@id


	UPDATE	[dbo].[sys_notifyMessage] SET gSendKey=@iii WHERE gSendKey=@id
	UPDATE	[dbo].[sys_notifyMessage] SET gReceiveKey=@iii WHERE gReceiveKey=@id

	UPDATE	[dbo].[sys_role] SET gInsertKey=@iii WHERE gInsertKey=@id
	UPDATE	[dbo].[sys_role] SET gUpdateKey=@iii WHERE gUpdateKey=@id
	
	UPDATE	[dbo].[sys_role_menu] SET gInsertKey=@iii WHERE gInsertKey=@id
	UPDATE	[dbo].[sys_role_menu] SET gUpdateKey=@iii WHERE gUpdateKey=@id

	UPDATE	[dbo].[sys_user_goods] SET gUserKey=@iii WHERE gUserKey=@id

	UPDATE	[dbo].[sys_validate] SET gUserKey=@iii WHERE gUserKey=@id
	UPDATE	[dbo].[sys_vipHistory] SET gUserKey=@iii WHERE gUserKey=@id
	SET @iii=@iii+1
	 FETCH NEXT FROM sss INTO @id
	 PRINT(@iii)
　　END
　　CLOSE sss /* 关闭游标 */
DEALLOCATE sss /* 删除游标 */

GO

ALTER TABLE dbo.sys_userInfo ALTER COLUMN gkey BIGINT NOT NULL 
ALTER TABLE dbo.sys_userInfo ALTER COLUMN iSex TINYINT
IF EXISTS(SELECT * FROM sys.default_constraints WHERE name='DF__sys_userI__iEmai__2A164134')
BEGIN
    ALTER TABLE dbo.sys_userInfo  DROP   CONSTRAINT DF__sys_userI__iEmai__2A164134 
END
IF EXISTS(SELECT * FROM sys.default_constraints WHERE name='DF_sys_userInfo_iState')
BEGIN
    ALTER TABLE dbo.sys_userInfo  DROP   CONSTRAINT DF_sys_userInfo_iState 
END
ALTER TABLE dbo.sys_userInfo ALTER COLUMN iState TINYINT NOT NULL

ALTER TABLE dbo.sys_userInfo ALTER COLUMN iFlag TINYINT NOT NULL
ALTER TABLE dbo.sys_userInfo ALTER COLUMN iEmailValidate BIT
ALTER TABLE dbo.sys_userInfo ALTER COLUMN iSex TINYINT

ALTER TABLE dbo.sys_userInfo ADD PRIMARY KEY(gkey)
go

--菜单
EXEC sp_rename 'sys_menu.gInsertKey','gCreateKey'
EXEC sp_rename 'sys_menu.dInsert','dCreateTime'
EXEC sp_rename 'sys_menu.gUpdateKey','gUpdateKey'
EXEC sp_rename 'sys_menu.dUpdate','dUpdateTime'

ALTER TABLE dbo.sys_menu ALTER COLUMN iType TINYINT
ALTER TABLE dbo.sys_menu  DROP PK_sys_menu
ALTER TABLE dbo.sys_menu ALTER COLUMN gKey VARCHAR(36) NOT NULL 
ALTER TABLE dbo.sys_menu ALTER COLUMN gParentKey VARCHAR(36) NOT NULL 
ALTER TABLE dbo.sys_role_menu DROP PK_sys_role_menu
ALTER TABLE dbo.[sys_role_menu] ALTER COLUMN gRoleKey VARCHAR(36) NOT NULL
ALTER TABLE dbo.[sys_role_menu] ALTER COLUMN gMenukey VARCHAR(36) NOT NULL
GO


DECLARE @iii BIGINT,@id VARCHAR(36)
SET @iii=1000001
DECLARE sss CURSOR
FOR SELECT gkey FROM dbo.sys_menu
OPEN sss
FETCH NEXT from sss INTO @id /* 读取第1行数据*/
　　WHILE @@FETCH_STATUS = 0 /* 用WHILE循环控制游标活动 */
　　BEGIN
　　	UPDATE [dbo].[sys_role_menu] SET gMenuKey=@iii WHERE gMenuKey=@id
	UPDATE dbo.sys_menu SET gKey=@iii WHERE gKey=@id
	UPDATE dbo.sys_menu SET gParentKey=@iii WHERE gParentKey=@id
	SET @iii=@iii+1
	 FETCH NEXT FROM sss INTO @id
	 PRINT(@iii)
　　END
　　CLOSE sss /* 关闭游标 */
DEALLOCATE sss /* 删除游标 */

UPDATE dbo.sys_menu SET gParentKey ='1000000' WHERE gParentKey='00000000-0000-0000-0000-000000000000'

ALTER TABLE dbo.sys_menu ALTER COLUMN gKey BIGINT NOT NULL 
ALTER TABLE dbo.sys_menu ALTER COLUMN gParentKey BIGINT NOT NULL 


GO

--角色
