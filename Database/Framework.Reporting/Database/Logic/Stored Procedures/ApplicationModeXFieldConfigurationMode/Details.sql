/******************************************************************************
**		File: 
**		Name: ApplicationModeXFieldConfigurationModeDetails
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

CREATE Procedure dbo.ApplicationModeXFieldConfigurationModeDetails
(
		@ApplicationModeXFieldConfigurationModeId		INT			= NULL	
	,	@ApplicationId									INT			= NULL
	,	@ApplicationModeId								INT			= NULL	
	,	@FieldConfigurationModeId						INT			= NULL	
	,	@AuditId										INT					
	,	@AuditDate										DATETIME	= NULL	
	,	@SystemEntityType								VARCHAR(50)	= 'ApplicationModeXFieldConfigurationMode'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ApplicationModeXFieldConfigurationModeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.ApplicationModeXFieldConfigurationModeId	
		,	a.ApplicationId	
		,	a.ApplicationModeId						
		,	a.FieldConfigurationModeId								
		,	b.Name				AS	'ApplicationMode'			
		,	c.Name				AS	'FieldConfigurationMode'	
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'					
	FROM		dbo.ApplicationModeXFieldConfigurationMode	a
	INNER JOIN	dbo.ApplicationMode					    	b	ON	a.ApplicationModeId	=	b.ApplicationModeId
	INNER JOIN	dbo.FieldConfigurationMode					c	ON	a.FieldConfigurationModeId	=	c.FieldConfigurationModeId
	WHERE	a.ApplicationModeXFieldConfigurationModeId	=	ISNULL(@ApplicationModeXFieldConfigurationModeId,	a.ApplicationModeXFieldConfigurationModeId)	
	AND		a.ApplicationModeId							=	ISNULL(@ApplicationModeId,			a.ApplicationModeId)
	AND		a.FieldConfigurationModeId					=	ISNULL(@FieldConfigurationModeId,			a.FieldConfigurationModeId)
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationModeXFieldConfigurationModeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END

GO

