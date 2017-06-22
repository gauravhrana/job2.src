IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeCategoryDelete'
	DROP  Procedure FieldConfigurationModeCategoryDelete
END
GO

PRINT 'Creating Procedure FieldConfigurationModeCategoryDelete'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeCategoryDelete
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
**     ----------						-----------
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
CREATE Procedure dbo.FieldConfigurationModeCategoryDelete
(
			@FieldConfigurationModeCategoryId 	INT						
		,	@AuditId			INT						
		,	@AuditDate			DATETIME	= NULL		
		,	@SystemEntityType	VARCHAR(50)	= 'FieldConfigurationModeCategory'
)
AS
BEGIN
	DELETE	 dbo.FieldConfigurationModeCategory
	WHERE	 FieldConfigurationModeCategoryId = @FieldConfigurationModeCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FieldConfigurationModeCategory'
		,	@EntityKey				= @FieldConfigurationModeCategoryId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO