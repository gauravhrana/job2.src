IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyDetailDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SuperKeyDetailDeleteHard'
	DROP  Procedure SuperKeyDetailDeleteHard
END
GO

PRINT 'Creating Procedure SuperKeyDetailDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: SuperKeyDetailDelete
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
CREATE Procedure dbo.SuperKeyDetailDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'SuperKeyDetail'
)
AS
BEGIN

	IF @KeyType = 'SuperKeyDetailId'
	BEGIN

		DELETE	 dbo.SuperKeyDetail
		WHERE	 SuperKeyDetailId = @KeyId

	END

	
END
GO
