IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginDeleteHard')
BEGIN
	PRINT 'Dropping Procedure UserLoginDeleteHard'
	DROP  Procedure UserLoginDeleteHard
END
GO

PRINT 'Creating Procedure UserLoginDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: UserLoginDelete
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
**		Date:		Author:				RecordDate:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.UserLoginDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'UserLogin'
)
AS
BEGIN

	IF @KeyType = 'UserLoginId'
	BEGIN

		DELETE	 dbo.UserLogin
		WHERE	 UserLoginId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
