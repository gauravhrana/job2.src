IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogDetailUpdate')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDetailUpdate'
	DROP  Procedure  StoredProcedureLogDetailUpdate
END
GO

PRINT 'Creating Procedure StoredProcedureLogDetailUpdate'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedureLogDetailUpdate
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

CREATE Procedure dbo.StoredProcedureLogDetailUpdate
(
		@StoredProcedureLogDetailId		INT			= NULL 			
	,	@StoredProcedureLogId			INT			= NULL 			
	,	@ParameterName					VARCHAR(50)					
	,	@ParameterValue					VARCHAR(50)					
	,	@AuditId						INT							
	,	@AuditDate						DATETIME	= NULL			
	,	@SystemEntityType				VARCHAR(50)	= 'StoredProcedureLogDetail'
)
AS
BEGIN 

	UPDATE	dbo.StoredProcedureLogDetail 
	SET		StoredProcedureLogId		= @StoredProcedureLogId
		,	ParameterName				= @ParameterName
		,   ParameterValue				= @ParameterValue						
	WHERE	StoredProcedureLogDetailId	= @StoredProcedureLogDetailId

	
 END		
 GO