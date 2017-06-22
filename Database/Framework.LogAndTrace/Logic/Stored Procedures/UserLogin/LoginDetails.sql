/******************************************************************************
**		File: 
**		Name: LoginDetails
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				RecordDate:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.LoginDetails
(
	
		@FromDate				DECIMAL(15, 0)	= NULL 
	,	@ToDate				DECIMAL(15, 0)	= NULL    			
	
)
AS
BEGIN
    
SELECT count(*) AS 'Total Logins', UserName FROM userlogin where  
  substring((CAST(RecordDate AS CHAR(50))), 1,(len(RecordDate)-4))	>= @FromDate  	
  AND
  substring((CAST(RecordDate AS CHAR(50))), 1,(len(RecordDate)-4))  <= @ToDate
GROUP BY UserName 	
	
END

GO

