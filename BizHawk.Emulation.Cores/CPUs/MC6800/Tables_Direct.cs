using System;

namespace BizHawk.Emulation.Common.Cores.MC6800
{
	public partial class MC6800
	{
		// this contains the vectors of instrcution operations
		// NOTE: This list is NOT confirmed accurate for each individual cycle

		private void NOP_()
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						OP };
		}

		private void INC_16(ushort src_l, ushort src_h)
		{
			cur_instr = new ushort[]
						{INC16,  src_l, src_h,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						OP };
		}


		private void DEC_16(ushort src_l, ushort src_h)
		{
			cur_instr = new ushort[]
						{DEC16, src_l, src_h,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						OP };
		}

		private void ADD_16(ushort dest_l, ushort dest_h, ushort src_l, ushort src_h)
		{
			cur_instr = new ushort[]
						{ADD16, dest_l, dest_h, src_l, src_h,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						OP };
		}

		private void REG_OP(ushort operation, ushort dest, ushort src)
		{
			cur_instr = new ushort[]
						{operation, dest, src,
						IDLE,
						IDLE,
						OP };
		}

		private void STOP_()
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						STOP };
		}

		private void HALT_()
		{
			if (!FlagI)
			{
				cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						HALT };
			}
			else
			{
				// if interrupts are disabled,
				// a glitchy decrement to the program counter happens
				cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						OP_G};
			}
			
		}

		private void JR_COND(bool cond)
		{
			if (cond)
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							RD, W, PCl, PCh,
							IDLE,
							INC16, PCl, PCh,
							IDLE,
							ASGN, Z, 0,
							IDLE,
							ADDS, PCl, PCh, W, Z,
							IDLE,
							OP };
			}
			else
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							RD, Z, PCl, PCh,
							IDLE,
							INC16, PCl, PCh,
							IDLE,
							OP };
			}
		}

		private void JP_COND(bool cond)
		{
			if (cond)
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							RD, W, PCl, PCh,
							IDLE,
							INC16, PCl, PCh,
							IDLE,
							RD, Z, PCl, PCh,
							IDLE,
							INC16, PCl, PCh,
							IDLE,
							TR, PCl, W,
							IDLE,
							TR, PCh, Z,
							IDLE,
							OP };
			}
			else
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							RD, W, PCl, PCh,
							IDLE,
							INC16, PCl, PCh,
							IDLE,
							RD, Z, PCl, PCh,
							IDLE,
							INC16, PCl, PCh,
							IDLE,
							OP };
			}
		}

		private void RET_()
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						RD, PCl, SPl, SPh,
						IDLE,
						INC16, SPl, SPh,
						IDLE,
						RD, PCh, SPl, SPh,
						IDLE,
						INC16, SPl, SPh,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						OP };
		}

		private void RETI_()
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						RD, PCl, SPl, SPh,
						IDLE,
						INC16, SPl, SPh,
						IDLE,
						RD, PCh, SPl, SPh,
						IDLE,
						INC16, SPl, SPh,
						IDLE,
						EI_RETI,
						IDLE,
						IDLE,
						IDLE,
						OP };
		}


		private void RET_COND(bool cond)
		{
			if (cond)
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							IDLE,
							IDLE,
							IDLE,
							IDLE,
							RD, PCl, SPl, SPh,
							IDLE,
							INC16, SPl, SPh,
							IDLE,
							RD, PCh, SPl, SPh,
							IDLE,
							INC16, SPl, SPh,
							IDLE,
							IDLE,
							IDLE,
							IDLE,
							IDLE,
							OP };
			}
			else
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							IDLE,
							IDLE,
							IDLE,
							IDLE,
							OP };
			}
		}

		private void CALL_COND(bool cond)
		{
			if (cond)
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							RD, W, PCl, PCh,
							INC16, PCl, PCh,
							IDLE,							
							IDLE,
							RD, Z, PCl, PCh,
							INC16, PCl, PCh,
							IDLE,
							DEC16, SPl, SPh,
							IDLE,
							IDLE,
							IDLE,
							IDLE,
							WR, SPl, SPh, PCh,
							IDLE,							
							IDLE,
							DEC16, SPl, SPh,
							WR, SPl, SPh, PCl,
							IDLE,
							TR, PCl, W,
							TR, PCh, Z,
							OP };
			}
			else
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							RD, W, PCl, PCh,
							IDLE,
							INC16, PCl, PCh,
							IDLE,
							RD, Z, PCl, PCh,
							IDLE,
							INC16, PCl, PCh,
							IDLE,
							OP };
			}
		}

		private void BRS()
		{

			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						RD, ALU, PCl, PCh,
						INC16, PCl, PCh,
						IDLE,
						DEC16, SPl, SPh,
						WR, SPl, SPh, PCh,
						IDLE,
						IDLE,
						DEC16, SPl, SPh,
						WR, SPl, SPh, PCl,
						ASGN, W, 0,
						ADDS, PCl, PCh, ALU, W,
						OP };
		}

		private void INT_OP(ushort operation, ushort src)
		{
			cur_instr = new ushort[]
						{operation, src,
						OP };
		}

		private void BIT_OP(ushort operation, ushort bit, ushort src)
		{
			cur_instr = new ushort[]
						{operation, bit, src,
						IDLE,
						IDLE,
						OP };
		}

		private void PUSH_(ushort src)
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						DEC16, SPl, SPh,
						IDLE,
						WR, SPl, SPh, src,
						OP };
		}

		// NOTE: this is the only instruction that can write to F
		// but the bottom 4 bits of F are always 0, so instead of putting a special check for every read op
		// let's just put a special operation here specifically for F
		private void POP_(ushort src)
		{
			if (src != P)
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							RD, src, SPl, SPh,
							IDLE,
							INC16, SPl, SPh,
							IDLE,
							OP };
			}
			else
			{
				cur_instr = new ushort[]
							{IDLE,
							IDLE,
							IDLE,
							RD_F, src, SPl, SPh,
							IDLE,
							INC16, SPl, SPh,
							IDLE,
							OP };
			} 
		}

		private void RST_(ushort n)
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						DEC16, SPl, SPh,
						IDLE,						
						IDLE,					
						IDLE,
						WR, SPl, SPh, PCh,
						DEC16, SPl, SPh,
						IDLE,				
						IDLE,
						WR, SPl, SPh, PCl,
						IDLE,
						ASGN, PCh, 0,
						ASGN, PCl, n,
						OP };
		}

		private void DI_()
		{
			cur_instr = new ushort[]
						{DI,
						IDLE,
						IDLE,
						OP };
		}

		private void EI_()
		{
			cur_instr = new ushort[]
						{EI,
						IDLE,
						IDLE,
						OP };
		}

		private void ADD_SP()
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						IDLE,
						IDLE,
						RD, W, PCl, PCh,
						IDLE,
						INC16, PCl, PCh,
						IDLE,
						ASGN, Z, 0,
						IDLE,
						ADDS, SPl, SPh, W, Z,
						IDLE,
						IDLE,
						IDLE,
						OP };
		}

		private void JAM_()
		{
			cur_instr = new ushort[]
						{JAM,
						IDLE,
						IDLE,
						IDLE };
		}

		private void TR_16_(ushort dest_l, ushort dest_h, ushort src_l, ushort src_h)
		{
			cur_instr = new ushort[]
						{IDLE,
						TR_16, dest_l, dest_h, src_l, src_h,
						IDLE,
						OP };
		}

		private void CP_16_IMM(ushort src_l, ushort src_h)
		{
			cur_instr = new ushort[]
						{RD, Z, PCl, PCh,
						INC16, PCl, PCh,
						RD, W, PCl, PCh,
						INC16, PCl, PCh,
						CP16, Z, W, src_l, src_h,
						IDLE,
						OP };
		}

		private void REG_OP_IMM_INC(ushort operation, ushort dest, ushort src_l, ushort src_h)
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						RD, Z, src_l, src_h,
						IDLE,
						operation, dest, Z,
						INC16, src_l, src_h,
						OP };
		}


		private void LD_IMM_16(ushort dest_l, ushort dest_h, ushort src_l, ushort src_h)
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						RD, dest_l, src_l, src_h,
						IDLE,
						INC16, src_l, src_h,
						IDLE,
						RD, dest_h, src_l, src_h,
						IDLE,
						INC16, src_l, src_h,
						IDLE,
						OP };
		}

		private void LD_16_IMM(ushort dest_l, ushort dest_h, ushort src_l, ushort src_h)
		{
			cur_instr = new ushort[]
						{IDLE,
						IDLE,
						IDLE,
						WR, dest_l, dest_h, src_l,
						IDLE,
						INC16, dest_l, src_h,
						IDLE,
						WR, dest_l, dest_h, src_h,
						IDLE,
						INC16, dest_l, dest_h,
						IDLE,
						OP };
		}
	}
}