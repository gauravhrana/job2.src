IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeDelete')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeDelete'
	DROP  Procedure FieldConfigurationModeDelete
END
GO

PRINT 'Creating Procedure FieldConfigurationModeDelete'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeDelete
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
CREATE Procedure dbo.FieldConfigurationModeDelete
(
			@FieldConfigurationModeId 	INT						
		,	@AuditId			INT						
		,	@AuditDate			DATETIME	= NULL		
		,	@SystemEntityType	VARCHAR(50)	= 'FieldConfigurationMode'
)
AS
BEGIN
	DELETE	 dbo.FieldConfigurationMode
	WHERE	 FieldConfigurationModeId = @FieldConfigurationModeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FieldConfigurationMode'
		,	@EntityKey				= @FieldConfigurationModeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO