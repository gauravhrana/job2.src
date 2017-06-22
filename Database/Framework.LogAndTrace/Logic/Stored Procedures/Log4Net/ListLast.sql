IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'Log4NetListLast')
BEGIN
	PRINT 'Dropping Procedure Log4NetListLast'
	DROP  Procedure  dbo.Log4NetListLast
END
GO

PRINT 'Creating Procedure Log4NetListLast'
GO

/******************************************************************************
**		File: 
**		Level: Log4NetList
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
**     ----------					   ---------
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

CREATE Procedure dbo.Log4NetListLast
(
		@ApplicationId				INT				= NULL
	,	@ExcludeApplicationId		INT
	,	@LogUser					VARCHAR(255)	= NULL
	,	@NoOfRecords				INT		
	,	@ConnectionKey				VARCHAR(100)		= NULL	
	,	@AuditId					INT		
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'Log4Net'
)
AS
BEGIN	

	SET  ROWCOUNT @NoOfRecords

	SELECT 	a.Id
		,	a.LogUser
		,	a.ApplicationID
		,	a.Date
		,	a.StackTrace
		,	a.Thread
		,	a.Level
		,	a.Logger
		,	a.Message
		,	a.Computer
		,	a.Exception
		,	a.ConnectionKey 
	FROM	dbo.Log4Net a
	WHERE	a.ApplicationId =	ISNULL(@ApplicationId		, a.ApplicationId)
	AND		a.LogUser		=	ISNULL(@LogUser				, a.LogUser)
	AND		a.ApplicationId <>	ISNULL(@ExcludeApplicationId, -1)
	AND		a.ConnectionKey	= ISNULL(@ConnectionKey, a.ConnectionKey)
	--WHERE	
	ORDER BY a.Id		DESC

	SET  ROWCOUNT 0

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
		
GO