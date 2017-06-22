IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceKeyChildrenGet')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeyChildrenGet'
	DROP  Procedure UserPreferenceKeyChildrenGet
END
GO

PRINT 'Creating Procedure UserPreferenceKeyChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: UserPreferenceKeyChildrenGet
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
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.UserPreferenceKeyChildrenGet
(
		@UserPreferenceKeyId	INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'UserPreferenceKey'
)
AS
BEGIN

	-- GET UserPreference Records
	SELECT	a.UserPreferenceId												
		,	a.Value																
		,	a.DataTypeId						
		,	d.Name								AS 'UserPreferenceDataType'											
		,	a.ApplicationUserId															
		,	b.FirstName + ' ' + b.LastName		AS 'ApplicationUser'												
		,	a.ApplicationId	 
		,	e.Name								AS 'Application'															
		,	a.UserPreferenceKeyId			
		,	c.Name								AS 'UserPreferenceKey'
		,	a.UserPreferenceCategoryId
		,	f.Name								AS 'UserPreferenceCategory'
	FROM		dbo.UserPreference a
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	b ON a.ApplicationUserId		= b.ApplicationUserId
	INNER JOIN	dbo.UserPreferenceKey								c ON a.UserPreferenceKeyId		= c.UserPreferenceKeyId
	INNER JOIN	dbo.UserPreferenceDataType							d ON a.DataTypeId				= d.UserPreferenceDataTypeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application		e ON a.ApplicationId			= e.ApplicationId
	INNER JOIN	dbo.UserPreferenceCategory							f ON a.UserPreferenceCategoryId = f.UserPreferenceCategoryId	
	WHERE	a.UserPreferenceKeyId = @UserPreferenceKeyId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceKeyId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   