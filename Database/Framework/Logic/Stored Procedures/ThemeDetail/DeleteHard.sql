IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailDeleteHard'
	DROP  Procedure ThemeDetailDeleteHard
END
GO

PRINT 'Creating Procedure ThemeDetailDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ThemeDetailDelete
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
CREATE Procedure dbo.ThemeDetailDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeDetail'
)
AS
BEGIN
	IF @KeyType = 'ThemeDetailId'
	BEGIN

		DELETE	 dbo.ThemeDetail
		WHERE	 ThemeDetailId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
