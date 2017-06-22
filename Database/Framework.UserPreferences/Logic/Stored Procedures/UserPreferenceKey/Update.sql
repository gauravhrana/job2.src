IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceKeyUpdate')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeyUpdate'
	DROP  Procedure  UserPreferenceKeyUpdate
END
GO

PRINT 'Creating Procedure UserPreferenceKeyUpdate'
GO

/******************************************************************************
**		File: 
**		Name: UserPreferenceKeyUpdate
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.UserPreferenceKeyUpdate
(
		@UserPreferenceKeyId	INT			
	,	@Name					VARCHAR(50)			
	,	@Value					VARCHAR(50)			
	,	@DataTypeId				INT					
	,	@Description			VARCHAR(50)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50)	= 'UserPreferenceKey'
)
AS
BEGIN

	SET NOCOUNT ON
	
	UPDATE	dbo.UserPreferenceKey 
	SET		Name					=	@Name				
		,	Value					=	@Value				
		,	DataTypeId				=	@DataTypeId			
		,	Description				=	@Description		
		,	SortOrder				=	@SortOrder				
	WHERE	UserPreferenceKeyId		=	@UserPreferenceKeyId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceKeyId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO