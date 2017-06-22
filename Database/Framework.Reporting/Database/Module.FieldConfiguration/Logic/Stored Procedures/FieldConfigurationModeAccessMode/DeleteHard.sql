IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeAccessModeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeAccessModeDeleteHard'
	DROP  Procedure FieldConfigurationModeAccessModeDeleteHard
END
GO

PRINT 'Creating Procedure FieldConfigurationModeAccessModeDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: FieldConfigurationModeAccessModeDelete
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
CREATE Procedure dbo.FieldConfigurationModeAccessModeDeleteHard
(
		@KeyId 								INT					
	,	@KeyType							VARCHAR(50)				
	,	@AuditId							INT						
	,	@AuditDate							DATETIME		= NULL		
	,	@SystemEntityType					VARCHAR(50)		= 'FieldConfigurationModeAccessMode'
)
AS
BEGIN

	IF @KeyType = 'FieldConfigurationModeAccessModeId'
	BEGIN				

		DELETE	 dbo.FieldConfigurationModeAccessMode
		WHERE	 FieldConfigurationModeAccessModeId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
