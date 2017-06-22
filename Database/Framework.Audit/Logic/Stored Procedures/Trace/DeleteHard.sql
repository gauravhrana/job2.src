IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TraceDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TraceDeleteHard'
	DROP  Procedure TraceDeleteHard
END
GO

PRINT 'Creating Procedure TraceDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: TraceDelete
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
CREATE Procedure dbo.TraceDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'Trace'
)
AS
BEGIN
	IF @KeyType = 'TraceId'
	BEGIN

		DELETE	 dbo.Trace
		WHERE	 TraceId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
