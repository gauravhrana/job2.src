IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditActionDeleteHard')
BEGIN
	PRINT 'Dropping Procedure AuditActionDeleteHard'
	DROP  Procedure AuditActionDeleteHard
END
GO

PRINT 'Creating Procedure AuditActionDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: AuditActionDeleteHard
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

CREATE Procedure dbo.AuditActionDeleteHard
(
		@KeyId 				INT						
	,	@KeyType			VARCHAR(50)				
	,	@AuditId			INT						
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'AuditAction'
)
AS	
BEGIN
	
	IF @KeyType = 'AuditActionId'
		BEGIN
	
			DELETE	 dbo.AuditAction
			WHERE	 AuditActionId = @KeyId

	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
