
CREATE PROCEDURE ApplicationModeXFieldConfigurationModeSearch
(
		@ApplicationModeXFieldConfigurationModeId		INT				= NULL	
	,	@ApplicationModeId								INT				= NULL	
	,	@FieldConfigurationModeId						INT				= NULL	
	,	@AuditId										INT						
	,	@AuditDate										DATETIME		= NULL
	,	@SystemEntityType								VARCHAR(50)		= 'ApplicationModeXFieldConfigurationMode' 
)
AS
BEGIN
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


	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationModeXFieldConfigurationModeId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

