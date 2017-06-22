IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RunTimeFeatureList')
BEGIN
	PRINT 'Dropping Procedure RunTimeFeatureList'
	DROP  Procedure  dbo.RunTimeFeatureList
END
GO

PRINT 'Creating Procedure RunTimeFeatureList'
GO

/******************************************************************************
**		File: 
**		Name: RunTimeFeatureList
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

CREATE Procedure dbo.RunTimeFeatureList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'RunTimeFeature'
)
AS
BEGIN

	SELECT	a.RunTimeFeatureId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.Description	   
		,	a.SortOrder
	 FROM	dbo.RunTimeFeature a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.RunTimeFeatureId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO