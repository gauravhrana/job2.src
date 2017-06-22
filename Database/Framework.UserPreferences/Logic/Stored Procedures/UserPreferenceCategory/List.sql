IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceCategoryList')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceCategoryList'
	DROP  Procedure  dbo.UserPreferenceCategoryList
END
GO

PRINT 'Creating Procedure UserPreferenceCategoryList'
GO

/******************************************************************************
**		Task: 
**		Name: UserPreferenceCategoryList
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
**     ----------					   ---------
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

CREATE Procedure dbo.UserPreferenceCategoryList
(
		@AuditId				INT						
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'UserPreferenceCategory'
)
AS
BEGIN

	SELECT	UserPreferenceCategoryId
		,	ApplicationId
		,	Name			
		,	Description		
		,	SortOrder			
	FROM	dbo.UserPreferenceCategory
	WHERE	ApplicationId = @ApplicationId
	ORDER BY	UserPreferenceCategoryId			ASC
		,		SortOrder							ASC	

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO