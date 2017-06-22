IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDataTypeChildrenGet')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeChildrenGet'
	DROP  Procedure UserPreferenceDataTypeChildrenGet
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: UserPreferenceDataTypeChildrenGet
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

CREATE Procedure dbo.UserPreferenceDataTypeChildrenGet
(
		@UserPreferenceDataTypeId		INT					
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL   
	,	@SystemEntityType				VARCHAR(50) = 'UserPreferenceDataType'
)
AS
BEGIN

	-- GET UserPreference Records
	SELECT		a.UserPreferenceKeyId
		,		a.ApplicationId					
		,		a.Name		
		,		a.Value				
		,		a.Description				
		,		a.SortOrder						
		,		a.DataTypeId				
		,		b.Name					AS	'DataType'
	FROM		dbo.UserPreferenceKey a
	INNER JOIN	UserPreferenceDataType  b	ON	a.DataTypeId = b.UserPreferenceDataTypeId	
	WHERE	a.DataTypeId = @UserPreferenceDataTypeId

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
	WHERE	a.DataTypeId = @UserPreferenceDataTypeId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceDataTypeId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   