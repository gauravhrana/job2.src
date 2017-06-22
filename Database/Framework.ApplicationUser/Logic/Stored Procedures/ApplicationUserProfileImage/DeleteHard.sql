--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserProfileImageDeleteHard')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserProfileImageDeleteHard'
--	DROP  Procedure ApplicationUserProfileImageDeleteHard
--END
--GO

--PRINT 'Creating Procedure ApplicationUserProfileImageDeleteHard'
--GO

/******************************************************************************
**		Task: 
**		Name: ApplicationUserProfileImageDelete
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
CREATE Procedure dbo.ApplicationUserProfileImageDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT		 = NULL				
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'ApplicationUserProfileImage'	 
)
AS
BEGIN

	IF @KeyType = 'ApplicationUserProfileImageId'
		BEGIN

			DELETE	 dbo.ApplicationUserProfileImage
			WHERE	 ApplicationUserProfileImageId = @KeyId

		END
	
END
GO
