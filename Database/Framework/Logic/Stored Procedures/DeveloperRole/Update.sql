IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeveloperRoleUpdate')
BEGIN
	PRINT 'Dropping Procedure DeveloperRoleUpdate'
	DROP  Procedure  DeveloperRoleUpdate
END
GO

PRINT 'Creating Procedure DeveloperRoleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: DeveloperRoleUpdate
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.DeveloperRoleUpdate
(
		@DeveloperRoleId			INT				= NULL
	,	@ApplicationId			INT		 		 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'DeveloperRole'
)
AS
BEGIN
	UPDATE	dbo.DeveloperRole 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder
		, 	ApplicationId	=	@ApplicationId 							
	WHERE	DeveloperRoleId	=	@DeveloperRoleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DeveloperRole'
		,	@EntityKey				= @DeveloperRoleId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO