IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TaskEntityTypeDeleteHard'
	DROP  Procedure TaskEntityTypeDeleteHard
END
GO

PRINT 'Creating Procedure TaskEntityTypeDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: TaskEntityTypeDelete
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
CREATE Procedure dbo.TaskEntityTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TaskEntityType'
)
AS
BEGIN

	IF @KeyType = 'TaskEntityTypeId'
	BEGIN

		EXEC	dbo.TaskEntityDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'TaskEntityTypeId',
				@AuditId	=	@AuditId

		DELETE	 dbo.TaskEntityType
		WHERE	 TaskEntityTypeId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
