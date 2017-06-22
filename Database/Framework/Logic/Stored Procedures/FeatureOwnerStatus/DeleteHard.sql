IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FeatureOwnerStatusDeleteHard')
BEGIN
	PRINT 'Dropping Procedure FeatureOwnerStatusDeleteHard'
	DROP  Procedure FeatureOwnerStatusDeleteHard
END
GO

PRINT 'Creating Procedure FeatureOwnerStatusDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: FeatureOwnerStatusDelete
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
CREATE Procedure dbo.FeatureOwnerStatusDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'FeatureOwnerStatus'
)
AS
BEGIN
	IF @KeyType = 'FeatureOwnerStatusId'
	BEGIN

		DELETE	 dbo.FeatureOwnerStatus
		WHERE	 FeatureOwnerStatusId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
