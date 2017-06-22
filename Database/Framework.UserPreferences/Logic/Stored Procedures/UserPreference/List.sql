IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceList')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceList'
	DROP  Procedure  dbo.UserPreferenceList
END
GO

PRINT 'Creating Procedure UserPreferenceList'
GO

/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceList
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

CREATE Procedure dbo.UserPreferenceList
(
		@AuditId				INT			
	,	@ApplicationId			INT	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'UserPreference'
)
AS
BEGIN

	SELECT	a.UserPreferenceId												
		,	a.ApplicationUserId																
		,	a.UserPreferenceKeyId											
		,	a.Value															
		,	a.DataTypeId														
		,	a.ApplicationId		
		,	a.UserPreferenceCategoryId												
		,	b.FirstName + ' ' + b.LastName		AS 'ApplicationUser'					
		,	c.Name								AS 'UserPreferenceKey'		
		,	d.Name								AS 'UserPreferenceDataType' 
		,	e.Name								AS 'Application'
		,	f.Name								AS 'UserPreferenceCategory'
	FROM		dbo.UserPreference			a
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	b ON a.ApplicationUserId		= b.ApplicationUserId
	INNER JOIN	dbo.UserPreferenceKey								c ON a.UserPreferenceKeyId		= c.UserPreferenceKeyId
	INNER JOIN	dbo.UserPreferenceDataType							d ON a.DataTypeId				= d.UserPreferenceDataTypeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application		e ON a.ApplicationId			= e.ApplicationId
	INNER JOIN	dbo.UserPreferenceCategory							f ON a.UserPreferenceCategoryId = f.UserPreferenceCategoryId
	WHERE		a.ApplicationId = @ApplicationId
	ORDER BY	a.UserPreferenceId			ASC	

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END

GO
