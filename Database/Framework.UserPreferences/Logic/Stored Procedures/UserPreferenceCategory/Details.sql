IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceCategoryDetails')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceCategoryDetails'
	DROP  Procedure UserPreferenceCategoryDetails
END
GO

PRINT 'Creating Procedure UserPreferenceCategoryDetails'
GO


/******************************************************************************
**		Task: 
**		Name: UserPreferenceCategoryDetails
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

CREATE Procedure dbo.UserPreferenceCategoryDetails
(
		@UserPreferenceCategoryId		INT					
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL
	,	@SystemEntityType				VARCHAR(50) = 'UserPreferenceCategory'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@UserPreferenceCategoryId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	UserPreferenceCategoryId
		,	ApplicationId			
		,	Name							
		,	Description				
		,	SortOrder
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'
	FROM	dbo.UserPreferenceCategory 
	WHERE	UserPreferenceCategoryId = @UserPreferenceCategoryId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceCategoryId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   