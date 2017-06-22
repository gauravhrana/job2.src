IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CountryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure CountryDeleteHard'
	DROP  Procedure CountryDeleteHard
END
GO

PRINT 'Creating Procedure CountryDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: CountryDelete
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
CREATE Procedure dbo.CountryDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'Country'
)
AS
BEGIN
	IF @KeyType = 'CountryId'
	BEGIN

		DELETE	 dbo.Country
		WHERE	 CountryId = @KeyId
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
