IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceSelectedItemDetails')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSelectedItemDetails'
	DROP  Procedure UserPreferenceSelectedItemDetails
END
GO

PRINT 'Creating Procedure UserPreferenceSelectedItemDetails'
GO


/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceSelectedItemDetails
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

CREATE Procedure dbo.UserPreferenceSelectedItemDetails
(
		@UserPreferenceSelectedItemId	INT					
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50) = 'UserPreferenceSelectedItem'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@UserPreferenceSelectedItemId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT		

	SELECT	a.UserPreferenceSelectedItemId
		,	a.ApplicationId				
		,	a.ApplicationUserId			
		,	a.UserPreferenceKeyId			
		,	a.ParentKey					
		,	a.Value						
		,	a.SortOrder
		,	b.Name								AS	'UserPreferenceKey'
		,	@LastUpdatedDate					AS	'UpdatedDate'
		,	@LastUpdatedBy						AS	'UpdatedBy'
		,	@LastAuditAction					AS	'LastAction'	
	FROM		dbo.UserPreferenceSelectedItem						a
	INNER JOIN	dbo.UserPreferenceKey								b ON a.UserPreferenceKeyId		= b.UserPreferenceKeyId
	WHERE		UserPreferenceSelectedItemId = @UserPreferenceSelectedItemId		

	--Create Audit History
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceSelectedItemId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
   