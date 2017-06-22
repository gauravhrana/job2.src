IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeveloperRoleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure DeveloperRoleDeleteHard'
	DROP  Procedure DeveloperRoleDeleteHard
END
GO

PRINT 'Creating Procedure DeveloperRoleDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: DeveloperRoleDelete
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
CREATE Procedure dbo.DeveloperRoleDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'DeveloperRole'
)
AS
BEGIN
	IF @KeyType = 'DeveloperRoleId'
	BEGIN

		DELETE	 dbo.DeveloperRole
		WHERE	 DeveloperRoleId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
