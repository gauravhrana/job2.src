IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceUpdate')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceUpdate'
	DROP  Procedure  UserPreferenceUpdate
END
GO

PRINT 'Creating Procedure UserPreferenceUpdate'
GO

/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceUpdate
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

CREATE Procedure dbo.UserPreferenceUpdate
(
		@UserPreferenceId			INT		 		
	,	@ApplicationUserId			INT             	
	,	@UserPreferenceKeyId		INT				
	,	@UserPreferenceCategoryId	INT				
	,	@Value						VARCHAR(50)		
	,	@DataTypeId					INT				
	,	@AuditId					INT				
	,	@AuditDate					DATETIME = NULL	
	,	@SystemEntityType			VARCHAR(50) = 'UserPreference'
)
AS
BEGIN

	UPDATE	dbo.UserPreference 
	SET		ApplicationUserId			=	@ApplicationUserId						
		,	UserPreferenceKeyId			=	@UserPreferenceKeyId		
		,	UserPreferenceCategoryId	=	@UserPreferenceCategoryId		
		,	Value						=	@Value						
		,	DataTypeId					=	@DataTypeId			
	WHERE	UserPreferenceId			=	@UserPreferenceId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
 GO
