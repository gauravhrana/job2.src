IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DateRangeTitleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure DateRangeTitleDeleteHard'
	DROP  Procedure DateRangeTitleDeleteHard
END
GO

PRINT 'Creating Procedure DateRangeTitleDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: DateRangeTitleDelete
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
CREATE Procedure dbo.DateRangeTitleDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'DateRangeTitle'
)
AS
BEGIN
	IF @KeyType = 'DateRangeTitleId'
	BEGIN

		DELETE	 dbo.DateRangeTitle
		WHERE	 DateRangeTitleId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
