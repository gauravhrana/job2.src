IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'ConnectionStringDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringDeleteHard'
	DROP  Procedure ConnectionStringDeleteHard
END
GO

PRINT 'Creating Procedure ConnectionStringDeleteHard'
GO


/******************************************************************************
**		File: 
**		TableName: ConnectionStringDeleteHard
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
**		Date:		Author:				TableDescription:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ConnectionStringDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		INT			= 'ConnectionString'
)
AS
BEGIN

	IF @KeyType = 'ConnectionStringId'
		BEGIN	

			--EXEC	@KeyId		=	@KeyId 
			--	,	@KeyType	=	'ConnectionStringId'
			--	,	@AuditId	=	@AuditId
										
			
			DELETE	dbo.ConnectionString
			WHERE	ConnectionStringId = @KeyId
	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
