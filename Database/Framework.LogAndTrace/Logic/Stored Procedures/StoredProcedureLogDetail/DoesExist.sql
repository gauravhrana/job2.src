IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='StoredProcedureLogDetailDoesExist')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailDoesExist'
	DROP  Procedure  StoredProcedureLogDetailDoesExist
END
GO

PRINT 'Creating Procedure StoredProcedureLogDetailDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedureLogDetailDoesExist
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

Create procedure dbo.StoredProcedureLogDetailDoesExist
(
		@StoredProcedureLogId	INT			= NULL		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'StoredProcedureLogDetail'
)
AS
BEGIN	

	SELECT	a.*
	FROM	dbo.StoredProcedureLogDetail a
	WHERE	a.StoredProcedureLogId = @StoredProcedureLogId	

	
END
GO

