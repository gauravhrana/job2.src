IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='StoredProcedureLogInsert')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogInsert'
	DROP Procedure StoredProcedureLogInsert
END
GO

PRINT 'Creating Procedure StoredProcedureLogInsert'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedueLogInsert
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC StoredProcedueLogInsert NULL	, NULL	, NULL
			EXEC StoredProcedueLogInsert NULL	, 'K'	, NULL
			EXEC StoredProcedueLogInsert 1		, 'K'	, NULL
			EXEC StoredProcedueLogInsert 1		, NULL	, NULL
			EXEC StoredProcedueLogInsert NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.StoredProcedureLogInsert
(
		@StoredProcedureLogId	INT			= NULL OUTPUT			
	,	@Name					VARCHAR(50)		
	,	@InputParameters		VARCHAR(500)
	,   @InputValues			VARCHAR(5000)					
	,	@ExecutedBy				VARCHAR(50)	= 'System'				
	,	@TimeOfExecution		DATETIME	= NULL					
	,	@AuditId				INT			= NULL					
	,	@SystemEntityType		VARCHAR(50)	= 'StoredProcedureLog'
)
AS
BEGIN
EXEC LoggingAndTrace.dbo.StoredProcedureLogInsert
		@Name						= @Name
	,	@InputParameters			= @InputParameters
	,	@InputValues				= @InputValues
	
	
	
END	
GO