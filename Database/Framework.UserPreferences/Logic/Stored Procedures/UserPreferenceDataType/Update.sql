IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDataTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeUpdate'
	DROP  Procedure  UserPreferenceDataTypeUpdate
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: UserPreferenceDataTypeUpdate
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

CREATE Procedure dbo.UserPreferenceDataTypeUpdate
(
		@UserPreferenceDataTypeId				INT	
	,	@Name									VARCHAR(50)					
	,	@Description							VARCHAR(50)				
	,	@SortOrder								INT						
	,	@AuditId								INT						
	,	@AuditDate								DATETIME		= NULL		
	,	@SystemEntityType						VARCHAR(50)		= 'UserPreferenceDataType'
	
)
AS
BEGIN
	UPDATE	dbo.UserPreferenceDataType 
	SET		Name			=	@Name				
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder							
	WHERE	UserPreferenceDataTypeId		=	@UserPreferenceDataTypeId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
	     	@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceDataTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
 GO
