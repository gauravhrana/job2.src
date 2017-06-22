/******************************************************************************
**		File: 
**		Name: FCModeCategoryXFCModeList
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

CREATE Procedure dbo.FCModeCategoryXFCModeList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'FCModeCategoryXFCMode'
)
AS
BEGIN

	SELECT	a.FCModeCategoryXFCModeId
		,	a.ApplicationId		
		,	a.FieldConfigurationModeCategoryId					
		,	a.FieldConfigurationModeId							
		,	b.Name		AS	'FieldConfigurationModeCategory'			
		,	c.Name		AS	'FieldConfigurationMode'
	FROM		dbo.FCModeCategoryXFCMode	a
	INNER JOIN	dbo.FieldConfigurationModeCategory			b	ON	a.FieldConfigurationModeCategoryId	=	b.FieldConfigurationModeCategoryId
	INNER JOIN	dbo.FieldConfigurationMode					c	ON	a.FieldConfigurationModeId			=	c.FieldConfigurationModeId
	ORDER BY	a.FCModeCategoryXFCModeId					ASC
		,		a.FieldConfigurationModeCategoryId			ASC
		,		a.FieldConfigurationModeId					ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO

