IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginReport')
BEGIN
  PRINT 'Dropping Procedure UserLoginReport'
  DROP  Procedure UserLoginReport
END

GO

PRINT 'Creating Procedure UserLoginReport'
GO


/******************************************************************************
**		File: 
**		Name: UserLoginReport
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

CREATE Procedure dbo.UserLoginReport
(
		@UserName				VARCHAR(50)	
	,	@StartDate				DECIMAL(15, 0)	= NULL 
	,	@EndDate				DECIMAL(15, 0)	= NULL    			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'UserLogin'
)
AS
BEGIN
    
	SET @UserName	= ISNULL(@UserName, '%')
	SELECT	a.UserName			
		,	MIN(a.RecordDate) AS StartDate
		,	MAX(a.RecordDate) AS EndDate						
		,	Count(*)	AS NoOfLogins		
	FROM		dbo.UserLogin		a
	WHERE a.UserName LIKE @UserName	+ '%'
	AND   a.RecordDate >=	ISNULL(@StartDate, a.RecordDate) 
	AND   a.RecordDate <=	ISNULL(@EndDate, a.RecordDate) 
	GROUP BY a.UserName	
	
	
END
GO
   