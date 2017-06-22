-- =============================================
-- Script Template
-- =============================================
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBProjectNameDeleteHard')
BEGIN
	PRINT 'Dropping Procedure DBProjectNameDeleteHard'
	DROP  Procedure DBProjectNameDeleteHard
END
GO

PRINT 'Creating Procedure DBProjectNameDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: DBProjectNameDelete
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
CREATE Procedure dbo.DBProjectNameDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'DBProjectName'
)
AS
BEGIN
	IF @KeyType = 'DBProjectNameId'
	BEGIN

		DELETE	 dbo.DBProjectName
		WHERE	 DBProjectNameId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
