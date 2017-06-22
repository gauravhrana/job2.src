/******************************************************************************
**		File: 
**		Name: ApplicationModeXFieldConfigurationModeList
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
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationModeXFieldConfigurationModeList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationModeXFieldConfigurationMode'
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
	INNER JOIN	dbo.ApplicationMode							b	ON	a.ApplicationModeId	        =	b.ApplicationModeId
	INNER JOIN	dbo.FieldConfigurationMode					c	ON	a.FieldConfigurationModeId	=	c.FieldConfigurationModeId
	ORDER BY	a.ApplicationModeXFieldConfigurationModeId		ASC
		,		a.ApplicationModeId										ASC
		,		a.FieldConfigurationModeId						ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO

