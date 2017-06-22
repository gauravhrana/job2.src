IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RunTimeFeatureDeleteHard')
BEGIN
	PRINT 'Dropping Procedure RunTimeFeatureDeleteHard'
	DROP  Procedure RunTimeFeatureDeleteHard
END
GO

PRINT 'Creating Procedure RunTimeFeatureDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: RunTimeFeatureDelete
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
CREATE Procedure dbo.RunTimeFeatureDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'RunTimeFeature'
)
AS
BEGIN
	IF @KeyType = 'RunTimeFeatureId'
	BEGIN

		DELETE	 dbo.RunTimeFeature
		WHERE	 RunTimeFeatureId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
