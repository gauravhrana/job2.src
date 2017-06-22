IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationDisplayNameDeleteHard')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationDisplayNameDeleteHard'
	DROP  Procedure FieldConfigurationDisplayNameDeleteHard
END

GO

PRINT 'Creating Procedure FieldConfigurationDisplayNameDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: FieldConfigurationDisplayNameDeleteHard
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
CREATE Procedure dbo.FieldConfigurationDisplayNameDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)							
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'FieldConfigurationDisplayName'
)
AS
BEGIN
	
	SET NOCOUNT ON

	IF @KeyType = 'FieldConfigurationDisplayNameId'
		BEGIN

			DELETE	dbo.FieldConfigurationDisplayName 
			WHERE	FieldConfigurationDisplayNameId = @KeyId

		END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			
			@SystemEntityType		= 'FieldConfigurationDisplayName' 
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
