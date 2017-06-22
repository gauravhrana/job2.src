--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageMasterDeleteHard')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageMasterDeleteHard'
--	DROP  Procedure ApplicationUserProfileImageMasterDeleteHard
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageMasterDeleteHard'
--GO


/******************************************************************************
**		Task: 
**		Name: ApplicationUserProfileImageMasterDelete
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
CREATE Procedure dbo.ApplicationUserProfileImageMasterDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT		 = NULL				
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'ApplicationUserProfileImageMaster'	 
)
AS
BEGIN

	IF @KeyType = 'ApplicationUserProfileImageMasterId'
		BEGIN

			DELETE	 dbo.ApplicationUserProfileImageMaster
			WHERE	 ApplicationUserProfileImageMasterId = @KeyId

		END
	
END
GO
