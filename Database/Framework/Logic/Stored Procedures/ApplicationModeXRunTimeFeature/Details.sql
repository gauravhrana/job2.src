IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeXRunTimeFeatureDetails')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeXRunTimeFeatureDetails'
	DROP  Procedure ApplicationModeXRunTimeFeatureDetails
END
GO

PRINT 'Creating Procedure ApplicationModeXRunTimeFeatureDetails'
GO

/******************************************************************************
**		File: 
**		PersonId: ApplicationModeXRunTimeFeatureDetails
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

CREATE Procedure dbo.ApplicationModeXRunTimeFeatureDetails
(
		@ApplicationModeXRunTimeFeatureId		INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationModeXRunTimeFeature'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationModeXRunTimeFeatureId
		,	@SystemEntityTypeId		=	@SystemEntityTypeId
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT		

	SELECT	a.ApplicationModeXRunTimeFeatureId												
		,	a.ApplicationModeId																
		,	a.RunTimeFeatureId		
		,	a.ApplicationId	
		,	b.Name					AS  'ApplicationMode'
		,	c.Name					AS  'RunTimeFeature'		
		,	@LastUpdatedDate		AS	'UpdatedDate' 
		,	@LastUpdatedBy			AS	'UpdatedBy'   
		,	@LastAuditAction		AS	'LastAction'
	FROM		dbo.ApplicationModeXRunTimeFeature	a
	INNER JOIN	dbo.ApplicationMode			b ON a.ApplicationModeId			= b.ApplicationModeId
	INNER JOIN	dbo.RunTimeFeature			c ON a.RunTimeFeatureId				= c.RunTimeFeatureId
	
	WHERE	ApplicationModeXRunTimeFeatureId = @ApplicationModeXRunTimeFeatureId		

	--Create Audit History
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationModeXRunTimeFeatureId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
   