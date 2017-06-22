IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeDeleteHard'
	DROP  Procedure FieldConfigurationModeDeleteHard
END
GO

PRINT 'Creating Procedure FieldConfigurationModeDeleteHard'
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
CREATE Procedure dbo.FieldConfigurationModeDeleteHard
(
		@KeyId 								INT		
	,	@FieldConfigurationModeId		INT				= NULL				
	,	@KeyType							VARCHAR(50)				
	,	@AuditId							INT						
	,	@AuditDate							DATETIME		= NULL		
	,	@SystemEntityType					VARCHAR(50)		= 'FieldConfigurationMode'
)
AS
BEGIN
		IF @KeyType = 'FieldConfigurationModeId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'FieldConfigurationModeId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.FieldConfigurationMode
		WHERE	 FieldConfigurationModeId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
