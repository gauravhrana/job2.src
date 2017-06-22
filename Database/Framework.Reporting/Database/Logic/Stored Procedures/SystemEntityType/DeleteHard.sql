IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeDeleteHard'
	DROP  Procedure SystemEntityTypeDeleteHard
END
GO

PRINT 'Creating Procedure SystemEntityTypeDeleteHard'
GO


/******************************************************************************
**		File: 
**		TableName: SystemEntityTypeDeleteHard
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

CREATE Procedure dbo.SystemEntityTypeDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		INT			= 'SystemEntityType'
)
AS
BEGIN

	IF @KeyType = 'SystemEntityTypeId'
		BEGIN	

			--EXEC	@KeyId		=	@KeyId 
			--	,	@KeyType	=	'SystemEntityTypeId'
			--	,	@AuditId	=	@AuditId
										
			
			DELETE	dbo.SystemEntityType
			WHERE	SystemEntityTypeId = @KeyId
	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
