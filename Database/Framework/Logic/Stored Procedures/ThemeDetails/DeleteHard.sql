IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailsDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailsDeleteHard'
	DROP  Procedure ThemeDetailsDeleteHard
END
GO

PRINT 'Creating Procedure ThemeDetailsDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ThemeDetailsDelete
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
CREATE Procedure dbo.ThemeDetailsDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeDetails'
)
AS
BEGIN
	IF @KeyType = 'ThemeDetailId'
	BEGIN

		DELETE	 dbo.ThemeDetails
		WHERE	 ThemeDetailId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
