IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationModeCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationModeCategoryDeleteHard'
	DROP  Procedure FieldConfigurationModeCategoryDeleteHard
END
GO

PRINT 'Creating Procedure FieldConfigurationModeCategoryDeleteHard'
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
CREATE Procedure dbo.FieldConfigurationModeCategoryDeleteHard
(
		@KeyId 								INT		
	,	@FieldConfigurationModeCategoryId		INT				= NULL				
	,	@KeyType							VARCHAR(50)				
	,	@AuditId							INT						
	,	@AuditDate							DATETIME		= NULL		
	,	@SystemEntityType					VARCHAR(50)		= 'FieldConfigurationModeCategory'
)
AS
BEGIN
		IF @KeyType = 'FieldConfigurationModeCategoryId'
		BEGIN

		EXEC    @KeyId		=	@KeyId
				@KeyType	=	'FieldConfigurationModeCategoryId'
		,		@AuditId	=	@AuditId
				

		DELETE	 dbo.FieldConfigurationModeCategory
		WHERE	 FieldConfigurationModeCategoryId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
