IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeXRunTimeFeatureDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeXRunTimeFeatureDeleteHard'
	DROP  Procedure ApplicationModeXRunTimeFeatureDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationModeXRunTimeFeatureDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ApplicationModeXRunTimeFeatureDelete
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
CREATE Procedure dbo.ApplicationModeXRunTimeFeatureDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationModeXRunTimeFeature'
)
AS
BEGIN
	IF @KeyType = 'ApplicationModeXRunTimeFeatureId'
	BEGIN

		DELETE	 dbo.ApplicationModeXRunTimeFeature
		WHERE	 ApplicationModeXRunTimeFeatureId = @KeyId
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
