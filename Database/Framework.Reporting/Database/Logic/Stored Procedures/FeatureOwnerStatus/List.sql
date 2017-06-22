IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FeatureOwnerStatusList')
BEGIN
	PRINT 'Dropping Procedure FeatureOwnerStatusList'
	DROP  Procedure  dbo.FeatureOwnerStatusList
END
GO

PRINT 'Creating Procedure FeatureOwnerStatusList'
GO

/******************************************************************************
**		File: 
**		Name: FeatureOwnerStatusList
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

CREATE Procedure dbo.FeatureOwnerStatusList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'FeatureOwnerStatus'
)
AS
BEGIN

	SELECT	a.FeatureOwnerStatusId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.Description	   
		,	a.SortOrder
	 FROM	dbo.FeatureOwnerStatus a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.FeatureOwnerStatusId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO