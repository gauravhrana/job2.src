IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeXFieldConfigurationModeSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeXFieldConfigurationModeSearch'
	DROP  Procedure ApplicationModeXFieldConfigurationModeSearch
END
GO

PRINT 'Creating Procedure ApplicationModeXFieldConfigurationModeSearch'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationModeXFieldConfigurationModeSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC ApplicationModeXFieldConfigurationModeSearch NULL	, NULL	, NULL
			EXEC ApplicationModeXFieldConfigurationModeSearch NULL	, 'K'	, NULL
			EXEC ApplicationModeXFieldConfigurationModeSearch 1	, 'K'	, NULL
			EXEC ApplicationModeXFieldConfigurationModeSearch 1	, NULL	, NULL
			EXEC ApplicationModeXFieldConfigurationModeSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE PROCEDURE ApplicationModeXFieldConfigurationModeSearch
(
		@ApplicationModeXFieldConfigurationModeId		INT				= NULL	
	,	@ApplicationModeId								INT				= NULL	
	,	@FieldConfigurationModeId						INT				= NULL	
	,	@AuditId										INT						
	,	@AuditDate										DATETIME		= NULL
	,	@SystemEntityType								VARCHAR(50)		= 'ApplicationModeXFieldConfigurationMode' 	
	,	@ApplicationMode								INT				= NULL		
	,	@AddAuditInfo									INT				 = 1
	,	@AddTraceInfo									INT				 = 0
	,	@ReturnAuditInfo								INT				 = 0
)
WITH RECOMPILE
AS
BEGIN

	SET	NOCOUNT ON

	SELECT	a.ApplicationModeXFieldConfigurationModeId	
		,	a.ApplicationId	
		,	a.ApplicationModeId						
		,	a.FieldConfigurationModeId								
		,	b.Name		AS	'ApplicationMode'			
		,	c.Name		AS	'FieldConfigurationMode'
	FROM		dbo.ApplicationModeXFieldConfigurationMode	a
	INNER JOIN	ApplicationMode								b	ON	a.ApplicationModeId	=	b.ApplicationModeId
	INNER JOIN	FieldConfigurationMode						c	ON	a.FieldConfigurationModeId	=	c.FieldConfigurationModeId
	WHERE   a.ApplicationModeXFieldConfigurationModeId		= ISNULL(@ApplicationModeXFieldConfigurationModeId, a.ApplicationModeXFieldConfigurationModeId )
	AND		a.ApplicationModeId								= ISNULL(@ApplicationModeId, a.ApplicationModeId )
	AND		a.FieldConfigurationModeId						= ISNULL(@FieldConfigurationModeId, a.FieldConfigurationModeId )
	ORDER BY a.ApplicationModeXFieldConfigurationModeId	ASC


	IF @AddAuditInfo = 1 
		BEGIN
	
			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= @SystemEntityType
				,	@EntityKey				= @ApplicationModeXFieldConfigurationModeId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO

