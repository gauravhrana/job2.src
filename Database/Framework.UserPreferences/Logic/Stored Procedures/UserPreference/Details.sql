IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDetails')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDetails'
	DROP  Procedure UserPreferenceDetails
END
GO

PRINT 'Creating Procedure UserPreferenceDetails'
GO


/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceDetails
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

CREATE Procedure dbo.UserPreferenceDetails
(
		@UserPreferenceId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'UserPreference'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@UserPreferenceId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT		

	SELECT	a.UserPreferenceId												
		,	a.ApplicationUserId																
		,	a.UserPreferenceKeyId											
		,	a.Value															
		,	a.DataTypeId														
		,	a.ApplicationId		
		,	a.UserPreferenceCategoryId												
		,	b.FirstName + ' ' + b.LastName		AS	'ApplicationUser'					
		,	c.Name								AS	'UserPreferenceKey'		
		,	d.Name								AS	'UserPreferenceDataType' 
		,	e.Name								AS	'Application'
		,	f.Name								AS	'UserPreferenceCategory'
		,	@LastUpdatedDate					AS	'UpdatedDate'
		,	@LastUpdatedBy						AS	'UpdatedBy'
		,	@LastAuditAction					AS	'LastAction'	
	FROM		dbo.UserPreference					a
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	b ON a.ApplicationUserId		= b.ApplicationUserId
	INNER JOIN	dbo.UserPreferenceKey								c ON a.UserPreferenceKeyId		= c.UserPreferenceKeyId
	INNER JOIN	dbo.UserPreferenceDataType							d ON a.DataTypeId				= d.UserPreferenceDataTypeId
	INNER JOIN	AuthenticationAndAuthorization.dbo.Application		e ON a.ApplicationId			= e.ApplicationId
	INNER JOIN	dbo.UserPreferenceCategory							f ON a.UserPreferenceCategoryId = f.UserPreferenceCategoryId
	WHERE		UserPreferenceId = @UserPreferenceId		

	--Create Audit History
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
   