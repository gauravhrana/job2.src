IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='StoredProcedureLogRawDoesExist')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawDoesExist'
	DROP  Procedure  StoredProcedureLogRawDoesExist
END
GO

PRINT 'Creating Procedure StoredProcedureLogRawDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedureLogRawDoesExist
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

Create procedure dbo.StoredProcedureLogRawDoesExist
(
		@StoredProcedureLogId	INT			= NULL		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'StoredProcedureLogRaw'
)
AS
BEGIN	

	SELECT	a.*
	FROM	dbo.StoredProcedureLogRaw a
	WHERE	a.StoredProcedureLogId = @StoredProcedureLogId	

END
GO

