IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationDelete')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationDelete'
	DROP  Procedure FieldConfigurationDelete
END
GO

PRINT 'Creating Procedure FieldConfigurationDelete'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationDelete
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
CREATE Procedure dbo.FieldConfigurationDelete
(
		@FieldConfigurationId 			INT	
	,	@AuditId						INT						
	,	@AuditDate						DATETIME	= NULL
	,	@SystemEntityType				VARCHAR(50) = 'FieldConfiguration'			
)
AS
BEGIN

	DELETE	 dbo.FieldConfiguration
	WHERE	 FieldConfigurationId = @FieldConfigurationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	 
		,	@EntityKey				= @FieldConfigurationId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END

GO

