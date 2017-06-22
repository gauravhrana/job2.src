IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TimeZoneDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TimeZoneDeleteHard'
	DROP  Procedure TimeZoneDeleteHard
END
GO

PRINT 'Creating Procedure TimeZoneDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: TimeZoneDelete
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
CREATE Procedure dbo.TimeZoneDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'TimeZone'
)
AS
BEGIN
	IF @KeyType = 'TimeZoneId'
	BEGIN

		DELETE	 dbo.TimeZone
		WHERE	 TimeZoneId = @KeyId
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
