IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeXRunTimeFeatureList')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeXRunTimeFeatureList'
	DROP  Procedure  dbo.ApplicationModeXRunTimeFeatureList
END
GO

PRINT 'Creating Procedure ApplicationModeXRunTimeFeatureList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationModeXRunTimeFeatureList
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationModeXRunTimeFeatureList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationModeXRunTimeFeature'
)
AS
BEGIN

	SELECT	a.ApplicationModeXRunTimeFeatureId												
		,	a.ApplicationModeId														
		,	a.RunTimeFeatureId
		,	a.ApplicationId	
		,	b.Name					AS 'ApplicationMode'		
		,	c.Name					AS 'RunTimeFeature'

	FROM		dbo.ApplicationModeXRunTimeFeature	a
	INNER JOIN	dbo.ApplicationMode			b ON a.ApplicationModeId			= b.ApplicationModeId	
	INNER JOIN	dbo.RunTimeFeature			c ON a.RunTimeFeatureId				= c.RunTimeFeatureId
-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO