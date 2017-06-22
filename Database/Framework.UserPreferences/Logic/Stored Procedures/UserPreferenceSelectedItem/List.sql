IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceSelectedItemList')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSelectedItemList'
	DROP  Procedure  dbo.UserPreferenceSelectedItemList
END
GO

PRINT 'Creating Procedure UserPreferenceSelectedItemList'
GO

/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceSelectedItemList
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

CREATE Procedure dbo.UserPreferenceSelectedItemList
(
		@AuditId				INT			
	,	@ApplicationId			INT	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'UserPreferenceSelectedItem'
)
AS
BEGIN

	SELECT	a.UserPreferenceSelectedItemId
		,	a.ApplicationId				
		,	a.ApplicationUserId			
		,	a.UserPreferenceKeyId			
		,	a.ParentKey					
		,	a.Value						
		,	a.SortOrder
		,	b.Name								AS	'UserPreferenceKey'
	FROM		dbo.UserPreferenceSelectedItem						a
	INNER JOIN	dbo.UserPreferenceKey								b ON a.UserPreferenceKeyId		= b.UserPreferenceKeyId
	WHERE		a.ApplicationId = @ApplicationId
	ORDER BY	a.SortOrder			ASC	

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END

GO
