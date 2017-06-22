IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationDeleteHard')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationDeleteHard'
	DROP  Procedure FieldConfigurationDeleteHard
END
GO

PRINT 'Creating Procedure FieldConfigurationDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationDeleteHard
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
CREATE PROCEDURE dbo.FieldConfigurationDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50) = 'FieldConfiguration'
)
AS
BEGIN

	IF @KeyType = 'FieldConfigurationId'
	BEGIN

		DELETE	 dbo.FieldConfiguration
		WHERE	 FieldConfigurationId = @KeyId
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END

GO

